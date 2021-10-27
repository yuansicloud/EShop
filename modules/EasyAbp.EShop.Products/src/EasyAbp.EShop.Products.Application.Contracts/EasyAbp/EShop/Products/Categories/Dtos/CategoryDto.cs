namespace EasyAbp.EShop.Products.Categories.Dtos
{
    using EasyAbp.EShop.Products.Helpers;
    using JetBrains.Annotations;
    using System;
    using System.Collections.Generic;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// ��Ʒ���ģ��
    /// </summary>
    [Serializable]
    public class CategoryDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        /// <summary>
        /// ����
        /// </summary>
        public string UniqueName { get; set; }

        /// <summary>
        /// ��ʾ����
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        [NotNull]
        public string Code { get; set; }

        /// <summary>
        /// �㼶
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// ��ID
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// �Ӽ�
        /// </summary>
        public ICollection<CategoryDto> Children { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ý���ļ�
        /// </summary>
        public string MediaResources { get; set; }

        /// <summary>
        /// ���ļ�����ʾ
        /// </summary>
        public string LevelToChinese
        {
            get
            {
                return Level.ToCNNumberString();
            }
        }

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsHidden { get; set; }
    }
}
