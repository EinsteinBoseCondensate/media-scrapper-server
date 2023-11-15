﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Common.ServiceArguments;
public class PaginationParams<T>
{
    public Expression<Func<T, object>> SortExpression { get; set; }
    public int Limit { get; set; }
    public int Skip { get; set; }

    public PaginationParams(Expression<Func<T, object>> sortExpression, int limit, int skip = 0)
    {
        SortExpression = sortExpression;
        Limit = limit;
        Skip = skip;
    }
}
