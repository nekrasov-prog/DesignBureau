﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum InteractionStatus
    {
        [Display(Name = "Назначен")]
        Assigned, // Этот статус означает, что человеку поручена задача по работе с объектом в рамках проекта.
        [Display(Name = "Начат")]
        Started, // Этот статус означает, что человек начал выполнять задачу по работе с объектом.
        [Display(Name = "Завершен")]
        Completed, // Этот статус означает, что человек закончил выполнять задачу по работе с объектом и достиг желаемого результата.
        [Display(Name = "Проверен")]
        Verified, // Этот статус означает, что качество и соответствие работы с объектом были подтверждены другим человеком или системой.
        [Display(Name = "Одобрен")]
        Approved, // Этот статус означает, что работа с объектом была принята заказчиком или руководителем проекта.
        [Display(Name = "Идентифицирован")]
        Identified, // Этот статус означает, что человек определил тип и параметры артикулированного объекта с помощью видео или 3D-данных.
        [Display(Name = "Спланирован")]
        Planned, // Этот статус означает, что человек разработал последовательность действий для манипуляции с артикулированным объектом.
        [Display(Name = "Выполнен")]
        Executed, // Этот статус означает, что человек осуществил манипуляцию с артикулированным объектом в соответствии с планом.
        [Display(Name = "Оценен")]
        Evaluated, // Этот статус означает, что человек измерил эффект и качество манипуляции с артикулированным объектом.
        [Display(Name = "Скорректирован")]
        Adjusted // Этот статус означает, что человек изменил план или параметры манипуляции с артикулированным объектом на основе результатов оценки.
    }
}
