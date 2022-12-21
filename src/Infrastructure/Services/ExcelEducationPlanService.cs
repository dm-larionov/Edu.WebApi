using Edu.WebApi.Application.Interfaces.Services;
using Edu.WebApi.Domain.EducationProcess;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Edu.WebApi.Infrastructure.Services;

public class ExcelEducationPlanService : IExcelEducationPlanService
{

    public ExcelEducationPlanService()
    {
    }

    public async Task<Stream> ExportAsync(IReadOnlyCollection<DisciplineSemester> disciplineSemesters)
    {
        IWorkbook workbook = new XSSFWorkbook();

        (int, int) endedIndexes = GetHeadersOfEducationPlan(workbook, "Учебный план");
        int startRow = endedIndexes.Item1 + 1;
        int endCell = endedIndexes.Item2 + 1;

        ISheet sheet = workbook.GetSheet("Учебный план");
        IFont mainFont = CreateMainFontStyle(workbook, false);
        IFont mainBoldFont = CreateMainFontStyle(workbook, true);

        ICellStyle horisontalTextStyle = CreateHorisontalTextStyle(workbook, mainFont);
        ICellStyle horisontalBoldTextStyle = CreateHorisontalTextStyle(workbook, mainBoldFont);

        // TODO: Переработать индексы генерируемой шапки таблицы
        foreach (var educationCycle in disciplineSemesters.GroupBy(x =>
                     x.Discipline.EducationCycle))
        {
            var result1 =
                educationCycle.Where(x => x.CertificationForm is not null).Count(x => x.CertificationForm.Name.ToUpper().EndsWith("ЗАЧЁТ"));
            var result2 =
                educationCycle.Where(x => x.CertificationForm is not null).Count(x => x.CertificationForm.Name.ToUpper().EndsWith("ЭКЗАМЕН"));

            SetCellValue(sheet, startRow + 1, 1, educationCycle.Key.EducationCycleIndex);
            SetCellValue(sheet, startRow + 1, 2, educationCycle.Key.Name);
            SetCellValue(sheet, startRow + 1, 3, result1);
            SetCellValue(sheet, startRow + 1, 4, result2);
            SetCellValue(sheet, startRow + 1, 5, educationCycle.Sum(x => x.TheoryLessonHours));
            SetCellValue(sheet, startRow + 1, 6, educationCycle.Sum(x => x.PracticeWorkHours));
            SetCellValue(sheet, startRow + 1, 7, educationCycle.Sum(x => x.LaboratoryWorkHours));
            SetCellValue(sheet, startRow + 1, 8, educationCycle.Sum(x => x.ControlWorkHours));
            SetCellValue(sheet, startRow + 1, 9, educationCycle.Sum(x => x.IndependentWorkHours));
            SetCellValue(sheet, startRow + 1, 10, educationCycle.Sum(x => x.ConsultationHours));
            SetCellValue(sheet, startRow + 1, 11, educationCycle.Sum(x => x.ExamHours));
            SetCellValue(sheet, startRow + 1, 12, educationCycle.Sum(x => x.EducationalPracticeHours));
            SetCellValue(sheet, startRow + 1, 13, educationCycle.Sum(x => x.ProductionPracticeHours));

            SetCellValue(sheet, startRow + 1, 14, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 1).ToArray()));
            SetCellValue(sheet, startRow + 1, 15, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 2).ToArray()));
            SetCellValue(sheet, startRow + 1, 16, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 3).ToArray()));
            SetCellValue(sheet, startRow + 1, 17, 0);
            SetCellValue(sheet, startRow + 1, 18, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 4).ToArray()));
            SetCellValue(sheet, startRow + 1, 19, 0);
            SetCellValue(sheet, startRow + 1, 20, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 5).ToArray()));
            SetCellValue(sheet, startRow + 1, 21, 0);
            SetCellValue(sheet, startRow + 1, 22, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 6).ToArray()));
            SetCellValue(sheet, startRow + 1, 23, 0);
            SetCellValue(sheet, startRow + 1, 24, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 7).ToArray()));
            SetCellValue(sheet, startRow + 1, 25, 0);
            SetCellValue(sheet, startRow + 1, 26, GetTotalHours(educationCycle.Where(x => x.SemesterNumber == 8).ToArray()));
            SetCellValue(sheet, startRow + 1, 27, 0);

            for (int column = 0; column < endCell; column++)
            {
                SetCellStyle(sheet, startRow + 1, column, horisontalBoldTextStyle);
            }

            startRow += 1;

            foreach (var discipline in educationCycle.GroupBy(x => x.DisciplineId))
            {
                for (int column = 0; column < endCell; column++)
                {
                    SetCellStyle(sheet, startRow + 1, column, horisontalTextStyle);
                }

                WriteDisciplineTotalHours(sheet, startRow, discipline.ToArray());
                WriteTotalBySemester(sheet, startRow, discipline.ToArray());

                startRow += 1;
            }
        }

        return new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(await ObjectToByteArray(workbook))));
    }

    private void WriteDisciplineTotalHours(ISheet sheet, int startRow, IReadOnlyCollection<DisciplineSemester> disciplineSemester)
    {
        string result1 = string.Empty;
        string result2 = string.Empty;

        var zachetDisciplineSemesters =
            disciplineSemester.Where(x => x.CertificationForm is not null).Where(x => x.CertificationForm.Name.ToUpper().EndsWith("ЗАЧЁТ")).ToArray();
        var examDisciplineSemesters =
            disciplineSemester.Where(x => x.CertificationForm is not null).Where(x => x.CertificationForm.Name.ToUpper().EndsWith("ЭКЗАМЕН")).ToArray();
        if (zachetDisciplineSemesters.Length > 0)
        {
            foreach (var zachetDiscipline in zachetDisciplineSemesters.Where(x => x.CertificationForm is not null).OrderBy(x => x.SemesterNumber))
            {
                if (result1 != string.Empty)
                    result1 += ", ";

                Array.ForEach(
                    zachetDiscipline.CertificationForm.Name.ToUpper()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    s => result1 += s.FirstOrDefault());
                result1 += $"({zachetDiscipline.SemesterNumber})";
            }
        }

        if (examDisciplineSemesters.Length > 0)
        {
            foreach (var examDiscipline in examDisciplineSemesters.Where(x => x.CertificationForm is not null).OrderBy(x => x.SemesterNumber))
            {
                if (result2 != string.Empty)
                    result2 += ", ";

                Array.ForEach(
                    examDiscipline.CertificationForm.Name.ToUpper()
                        .Split(' ', StringSplitOptions.RemoveEmptyEntries),
                    s => result2 += s.FirstOrDefault());
                result2 += $"({examDiscipline.SemesterNumber}) ";
            }
        }

        SetCellValue(sheet, startRow + 1, 1, disciplineSemester.First().Discipline.DisciplineIndex);
        SetCellValue(sheet, startRow + 1, 2, disciplineSemester.First().Discipline.Name);
        SetCellValue(sheet, startRow + 1, 3, result1);
        SetCellValue(sheet, startRow + 1, 4, result2);
        SetCellValue(sheet, startRow + 1, 5, disciplineSemester.Sum(x => x.TheoryLessonHours));
        SetCellValue(sheet, startRow + 1, 6, disciplineSemester.Sum(x => x.PracticeWorkHours));
        SetCellValue(sheet, startRow + 1, 7, disciplineSemester.Sum(x => x.LaboratoryWorkHours));
        SetCellValue(sheet, startRow + 1, 8, disciplineSemester.Sum(x => x.ControlWorkHours));
        SetCellValue(sheet, startRow + 1, 9, disciplineSemester.Sum(x => x.IndependentWorkHours));
        SetCellValue(sheet, startRow + 1, 10, disciplineSemester.Sum(x => x.ConsultationHours));
        SetCellValue(sheet, startRow + 1, 11, disciplineSemester.Sum(x => x.ExamHours));
        SetCellValue(sheet, startRow + 1, 12, disciplineSemester.Sum(x => x.EducationalPracticeHours));
        SetCellValue(sheet, startRow + 1, 13, disciplineSemester.Sum(x => x.ProductionPracticeHours));
    }

    private int GetTotalHours(DisciplineSemester disciplineSemester)
    {
        if (disciplineSemester is null)
            return 0;

        return disciplineSemester.TheoryLessonHours
               + disciplineSemester.PracticeWorkHours
               + disciplineSemester.LaboratoryWorkHours
               + disciplineSemester.ControlWorkHours
               + disciplineSemester.IndependentWorkHours
               + disciplineSemester.ConsultationHours
               + disciplineSemester.ExamHours
               + disciplineSemester.EducationalPracticeHours
               + disciplineSemester.ProductionPracticeHours;
    }

    private int GetTotalHours(IReadOnlyCollection<DisciplineSemester> disciplineSemesters)
    {
        if (disciplineSemesters is null)
            return 0;

        int result = 0;
        foreach (var disciplineSemester in disciplineSemesters)
        {
            result += disciplineSemester.TheoryLessonHours
                 + disciplineSemester.PracticeWorkHours
                 + disciplineSemester.LaboratoryWorkHours
                 + disciplineSemester.ControlWorkHours
                 + disciplineSemester.IndependentWorkHours
                 + disciplineSemester.ConsultationHours
                 + disciplineSemester.ExamHours
                 + disciplineSemester.EducationalPracticeHours
                 + disciplineSemester.ProductionPracticeHours;
        }

        return result;
    }
    private void WriteTotalBySemester(ISheet sheet, int startRow, IReadOnlyCollection<DisciplineSemester> disciplineSemester)
    {
        SetCellValue(sheet, startRow + 1, 14, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 1)));
        SetCellValue(sheet, startRow + 1, 15, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 2)));
        SetCellValue(sheet, startRow + 1, 16, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 3)));
        SetCellValue(sheet, startRow + 1, 17, 0);
        SetCellValue(sheet, startRow + 1, 18, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 4)));
        SetCellValue(sheet, startRow + 1, 19, 0);
        SetCellValue(sheet, startRow + 1, 20, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 5)));
        SetCellValue(sheet, startRow + 1, 21, 0);
        SetCellValue(sheet, startRow + 1, 22, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 6)));
        SetCellValue(sheet, startRow + 1, 23, 0);
        SetCellValue(sheet, startRow + 1, 24, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 7)));
        SetCellValue(sheet, startRow + 1, 25, 0);
        SetCellValue(sheet, startRow + 1, 26, GetTotalHours(disciplineSemester.FirstOrDefault(x => x.SemesterNumber == 8)));
        SetCellValue(sheet, startRow + 1, 27, 0);
    }

    private async Task<byte[]> ObjectToByteArray(IWorkbook workbook)
    {
        await using MemoryStream ms = new MemoryStream();
        workbook.Write(ms);
        return ms.ToArray();
    }

    private static void SetRowHeight(ISheet worksheet, int rowPosition, short height)
    {
        IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
        dataRow.Height = height;
    }

    private static void SetCellStyle(ISheet worksheet, int rowPosition, int columnPosition, ICellStyle style)
    {
        IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
        ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
        cell.CellStyle = style;
    }
    private static void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, string value)
    {
        IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
        ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
        cell.SetCellValue(value);
    }

    private static void SetCellValue(ISheet worksheet, int rowPosition, int columnPosition, double value)
    {
        IRow dataRow = worksheet.GetRow(rowPosition) ?? worksheet.CreateRow(rowPosition);
        ICell cell = dataRow.GetCell(columnPosition) ?? dataRow.CreateCell(columnPosition);
        cell.SetCellValue(value);
    }

    private static IFont CreateMainFontStyle(IWorkbook workbook, bool isBold)
    {
        var font = workbook.CreateFont();
        font.FontHeightInPoints = 12;
        font.FontName = "Times New Roman";
        font.IsBold = isBold;

        return font;
    }

    private static void AddMergedStyledRegion(ISheet sheet, CellRangeAddress cellRange, ICellStyle style)
    {
        for (int row = cellRange.FirstRow; row <= cellRange.LastRow; row++)
        {
            for (int column = cellRange.FirstColumn; column <= cellRange.LastColumn; column++)
            {
                SetCellStyle(sheet, row, column, style);
            }
        }
        sheet.AddMergedRegion(cellRange);
    }

    private static ICellStyle CreateVerticalTextStyle(IWorkbook workbook, IFont? font = null)
    {
        var verticalTextStyle = workbook.CreateCellStyle();
        verticalTextStyle.WrapText = true;
        verticalTextStyle.Rotation = 90;
        verticalTextStyle.Alignment = HorizontalAlignment.Left;
        verticalTextStyle.VerticalAlignment = VerticalAlignment.Bottom;
        verticalTextStyle.BorderBottom = BorderStyle.Thin;
        verticalTextStyle.BorderTop = BorderStyle.Thin;
        verticalTextStyle.BorderLeft = BorderStyle.Thin;
        verticalTextStyle.BorderRight = BorderStyle.Thin;
        verticalTextStyle.BorderDiagonal = BorderDiagonal.Both;
        if (font != null)
            verticalTextStyle.SetFont(font);

        return verticalTextStyle;
    }

    private static ICellStyle CreateHorisontalTextStyle(IWorkbook workbook, IFont? font = null)
    {
        var horizontalTextStyle = workbook.CreateCellStyle();
        horizontalTextStyle.WrapText = true;
        horizontalTextStyle.Alignment = HorizontalAlignment.Left;
        horizontalTextStyle.VerticalAlignment = VerticalAlignment.Center;
        horizontalTextStyle.BorderBottom = BorderStyle.Thin;
        horizontalTextStyle.BorderTop = BorderStyle.Thin;
        horizontalTextStyle.BorderLeft = BorderStyle.Thin;
        horizontalTextStyle.BorderRight = BorderStyle.Thin;
        if (font != null)
            horizontalTextStyle.SetFont(font);

        return horizontalTextStyle;
    }
    private static (int, int) GetHeadersOfEducationPlan(IWorkbook workbook, string sheetName)
    {
        IFont mainBoldFont = CreateMainFontStyle(workbook, true);
        IFont mainFont = CreateMainFontStyle(workbook, false);

        var verticalBoldTextStyle = CreateVerticalTextStyle(workbook, mainBoldFont);
        var horizontalBoldTextStyle = CreateHorisontalTextStyle(workbook, mainBoldFont);

        var verticalTextStyle = CreateVerticalTextStyle(workbook, mainFont);
        var horizontalTextStyle = CreateHorisontalTextStyle(workbook, mainFont);

        // Полотно
        ISheet sheet = workbook.CreateSheet(sheetName);

        // Первая строка "Учебный план"
        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 2, 26));
        SetCellValue(sheet, 0, 2, "Учебный план");

        // Вторая строка "для специальности ... гг."
        sheet.AddMergedRegion(new CellRangeAddress(1, 1, 2, 26));
        SetCellValue(sheet, 1, 2, "для специальности 09.02.07 Информационные системы и программирование. 2019-2020 г");

        const int headerRow = 2;
        int headerColumn = 1;
        const int stepDown = 4;

        // Колонка индекс
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Индекс");

        headerColumn++;
        // Плитка наименование дисциплин
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), horizontalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Наименование циклов, дисциплин, профессиональных модулей, МДК, практик");

        headerColumn++;
        // Колонка форма промежуточной аттестации (зачёты)
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Форма промежуточной аттестации (зачеты, дифференцированные зачеты)");

        headerColumn++;
        // Колонка форма промежуточной аттестации (Экзамены)
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Форма промежуточной аттестации (экзамен)");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Объем образовательной нагрузки");

        headerColumn++;
        // Колонка форма самостоятельная учебная нагрузка
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 1, headerColumn, "Самостоятельная учебная нагрузка");

        // Плитка учебная нагрузка обучающихся
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow, headerColumn, headerColumn + 7), horizontalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Учебная нагрузка обучающихся (час.)");

        headerColumn++;
        // Колонка Всего учебных занятий
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 3, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 3, headerColumn, "Всего учебных занятий");

        // Плитка взаимодействии с преподавателем
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 6), horizontalBoldTextStyle);
        SetCellValue(sheet, headerRow + 1, headerColumn, "Во взаимодействии с преподавателем");

        // Плитка нагрузка на дисциплины и МДК
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 2, headerColumn, headerColumn + 3), horizontalTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "Нагрузка на дисциплины и МДК");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 4, headerColumn, "Теоретическое обучение");
        // Плитка нагрузка на дисциплины и МДК
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 3, headerRow + 3, headerColumn, headerColumn + 2), horizontalBoldTextStyle);
        SetCellValue(sheet, headerRow + 3, headerColumn, "в т.ч. по учебным дисциплинам и МДК");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 4, headerColumn, "Лаб. и практ. занятия");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 4, headerColumn, "Курсовых работ (проектов)");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "По практике производственной и учебной");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "Консультации");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + stepDown, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "Промежуточная аттестация");

        headerColumn++;
        // Плитка Распределение учебной нагрузки по курсам и семестрам
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow, headerRow, headerColumn, headerColumn + 13), horizontalBoldTextStyle);
        SetCellValue(sheet, headerRow, headerColumn, "Распределение учебной нагрузки по курсам и семестрам (час. в семестр)");

        // Плитка I Курс
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 1), horizontalTextStyle);
        SetCellValue(sheet, headerRow + 1, headerColumn, "I курс");

        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 4, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "1 сем.**нед.17");

        headerColumn++;
        // Колонка форма объема образовательной нагрузки
        AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 4, headerColumn, headerColumn), verticalBoldTextStyle);
        SetCellValue(sheet, headerRow + 2, headerColumn, "2 сем.**22нед.");

        headerColumn++;

        for (int i = 2; i <= 4; i++)
        {
            AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 1, headerRow + 1, headerColumn, headerColumn + 3), horizontalTextStyle);
            SetCellValue(sheet, headerRow + 1, headerColumn, $"{i} курс");

            for (int j = 1; 0 <= j; j--)
            {
                int semNumber = i * 2 - j;
                AddMergedStyledRegion(sheet, new CellRangeAddress(headerRow + 2, headerRow + 3, headerColumn, headerColumn + 1), horizontalTextStyle);
                SetCellValue(sheet, headerRow + 2, headerColumn, $"{semNumber} сем.");

                // Колонка форма объема образовательной нагрузки
                SetCellStyle(sheet, headerRow + 4, headerColumn, verticalBoldTextStyle);
                SetCellValue(sheet, headerRow + 4, headerColumn, "Во взаимодействии ");
                sheet.SetColumnWidth(headerColumn, 1500);

                // Колонка форма объема образовательной нагрузки
                SetCellStyle(sheet, headerRow + 4, headerColumn + 1, verticalBoldTextStyle);
                SetCellValue(sheet, headerRow + 4, headerColumn + 1, "с/р");
                sheet.SetColumnWidth(headerColumn + 1, 1500);
                headerColumn += 2;
            }
        }


        // Определение ширины для колонок
        sheet.SetColumnWidth(1, 3500);
        sheet.SetColumnWidth(2, 11000);
        sheet.SetColumnWidth(3, 3200);
        // Определение высоты для строки
        SetRowHeight(sheet, 0, 400);
        SetRowHeight(sheet, 1, 400);
        SetRowHeight(sheet, 2, 700);
        SetRowHeight(sheet, 5, 600);

        for (int i = 1; i <= headerColumn - 1; i++)
        {
            SetCellStyle(sheet, headerRow + 5, i, horizontalBoldTextStyle);
            SetCellValue(sheet, headerRow + 5, i, i);
        }

        return (headerRow + 4, headerColumn - 1);
    }
}
