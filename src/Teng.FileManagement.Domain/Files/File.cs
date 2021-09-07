using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Threading;

namespace Teng.FileManagement.Files
{
    public class File : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; }

        [NotNull]
        public string FileName { get; private set; }

        [NotNull]
        public string BlobName { get; private set; }

        public long ByteSize { get; private set; }

        public File(Guid id, Guid? tenantId, [NotNull] string fileName, [NotNull] string blobName, long byteSize) : base(id)
        {
            TenantId = tenantId;
            FileName = Check.NotNullOrWhiteSpace(fileName, nameof(fileName));
            BlobName = Check.NotNullOrWhiteSpace(blobName, nameof(blobName));
            ByteSize = byteSize;
        }

        public File()
        {
        }
    }
}