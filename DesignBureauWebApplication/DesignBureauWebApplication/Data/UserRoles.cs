using DesignBureauWebApplication.Models;

namespace DesignBureauWebApplication.Data
{
    public static class UserRoles
    {
        public const string Admin = "admin"; // администратор
        public const string User = "user"; // пользователь
        public const string ProjectManager = "project_manager"; // менеджер по проектам
        public const string Engineer = "engineer"; // инженер-конструктор
        public const string PurchasingManager = "purchasing_manager"; // менеджер по закупкам
        public const string QualityDepartmentSpecialist = "qd_specialist"; // специалист отдела качества
        public const string ProductionWorker = "p_worker"; // производственный рабочий
        public const string SupplyDepartmentWorker = "sd_worker"; // работник отдела снабжения
        public const string LogisticsManager = "logistics_manager"; // менеджер по логистике
        public const string Driver = "driver"; // перевозчик
        public const string Repair = "repair"; // специалист по ремонту
    }
}
