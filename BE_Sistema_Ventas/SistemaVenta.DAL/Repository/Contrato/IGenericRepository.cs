using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;


namespace SistemaVenta.DAL.Repository.Contrato
{
    public interface IGenericRepository<TModel> where TModel : class
    {
        Task<TModel> Get(Expression<Func<TModel, bool>>filtro);

        Task<TModel> Create(TModel modelo);

        Task<bool> Edit(TModel modelo);

        Task<bool> Delete(TModel modelo);

        Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>>filtro = null);
    }
}
