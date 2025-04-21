﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record PaginatedResult<TData>(int  pageIndex, int pageSize , int totalcount , IEnumerable<TData> data);
   

    
}
