using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Farmasi.Core.Domain
{
	public class BaseEntity
	{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

    }
}

