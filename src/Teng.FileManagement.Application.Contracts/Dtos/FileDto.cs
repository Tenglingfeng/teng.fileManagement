using System;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace Teng.FileManagement.Dtos
{
    public class FileDto : EntityDto
    {
        public string FileName { get; set; }

        public byte[] Bytes { get; set; }
    }
}