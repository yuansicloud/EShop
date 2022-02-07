using System;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Combinations.Combinations
{
    public class CombinationDetailHasBeenUsedException : BusinessException
    {
        public CombinationDetailHasBeenUsedException(Guid combinationDetailId) : base(
            message: $"CombinationDetail {combinationDetailId} has been used.")
        {
        }
    }
}
