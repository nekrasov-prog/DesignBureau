﻿using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DesignBureauWebApplication.Data.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "Создан📝")]
        Created, // это статус заказа, когда он только что сформирован и отправлен поставщику. Здесь указываются детали заказа, такие как номер, дата, стоимость, товары и количество
        [Display(Name = "Подтвержден📦")]
        Confirmed, // это статус заказа, когда поставщик принял его и начал готовить его к доставке. Здесь указываются детали подтверждения, такие как номер, дата и предполагаемое время доставки
        [Display(Name = "Отправлен🚚")]
        Shipped, // это статус заказа, когда он покинул склад поставщика и находится в пути к клиенту. Здесь указываются детали отгрузки, такие как номер, дата, перевозчик и трекинг-номер
        [Display(Name = "Доставлен✅")]
        Delivered, // это статус заказа, когда он прибыл на место назначения клиента и был им получен. Здесь указываются детали доставки, такие как номер, дата, подпись и отзыв
        [Display(Name = "Отменен❌")]
        Cancelled // это статус заказа, когда он был отменен одной из сторон до доставки. Здесь указываются детали отмены, такие как номер, дата, причина и возврат

    }
}
