using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans.Dtos;

[Serializable]
public class FlashSalePlanUpdateDto : ExtensibleEntityDto, IHasConcurrencyStamp
{
    public DateTime BeginTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public bool IsPublished { get; set; }

    public string ConcurrencyStamp { get; set; }
}