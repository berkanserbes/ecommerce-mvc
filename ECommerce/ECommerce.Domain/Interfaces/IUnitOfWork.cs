﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
	IProductRepository ProductRepository { get; }
	Task<int> SaveChangesAsync();
}
