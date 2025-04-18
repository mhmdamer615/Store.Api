using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public class SpecificationEvaluater
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> baseQuery, Specification<T> specification) where T : class
        {
            var query = baseQuery;
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }
            query = specification.Includes.Aggregate(query, (currentQuery, include) => currentQuery.Include(include));
            return query;
        }
    }
}
