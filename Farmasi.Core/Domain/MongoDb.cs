using System;
namespace Farmasi.Core.Domain
{
	public class MongoDb : Attribute
    {
        public string DbName { get; set; }
        public string CollectionName { get; set; }
    }
}

