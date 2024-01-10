using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaVenta.DAL.Repository.Contrato;
using SistemaVenta.DAL.DBContext;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;


namespace SistemaVenta.DAL.Repository
{
    public class GenericRepository<TModel>: IGenericRepository<TModel> where TModel : class
    {
        private readonly DbventaContext _dbcontext;

        public GenericRepository(DbventaContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<TModel> Get(Expression<Func<TModel, bool>> filtro)
        {
            try
            {
                TModel model = await _dbcontext.Set<TModel>().FirstOrDefaultAsync(filtro);

                return model;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<TModel> Create(TModel model)
        {
            try
            {
                _dbcontext.Set<TModel>().Add(model);
                await _dbcontext.SaveChangesAsync();

                return model;
            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Edit(TModel modelo)
        {
            try
            {
                _dbcontext.Set<TModel>().Update(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;

            }
            catch
            {
                throw;
            }
        }
        public async Task<bool> Delete(TModel modelo)
        {
            try
            {
                _dbcontext.Set<TModel>().Remove(modelo);
                await _dbcontext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IQueryable<TModel>> Consult(Expression<Func<TModel, bool>>filtro = null)
        {
            try
            {
                IQueryable<TModel>queryModelo = filtro == null? _dbcontext.Set<TModel>(): _dbcontext.Set<TModel>().Where(filtro);
                return queryModelo;
            }
            catch
            {
                throw;
            }
        }


    }
}
