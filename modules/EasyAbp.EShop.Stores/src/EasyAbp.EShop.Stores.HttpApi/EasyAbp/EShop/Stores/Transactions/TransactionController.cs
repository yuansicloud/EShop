namespace EasyAbp.EShop.Stores.Transactions
{
    using EasyAbp.EShop.Stores.Transactions.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 店铺流水控制器
    /// </summary>
    [RemoteService(Name = "EasyAbpEShopStores")]
    [Route("/api/e-shop/stores/transaction")]
    public class TransactionController : StoresController, ITransactionAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly ITransactionAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="ITransactionAppService"/>.</param>
        public TransactionController(ITransactionAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{TransactionDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TransactionDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="GetTransactionListInput"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{TransactionDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<TransactionDto>> GetListAsync(GetTransactionListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="CreateUpdateTransactionDto"/>.</param>
        /// <returns>The <see cref="Task{TransactionDto}"/>.</returns>
        [HttpPost]
        public Task<TransactionDto> CreateAsync(CreateUpdateTransactionDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="CreateUpdateTransactionDto"/>.</param>
        /// <returns>The <see cref="Task{TransactionDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TransactionDto> UpdateAsync(Guid id, CreateUpdateTransactionDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}
