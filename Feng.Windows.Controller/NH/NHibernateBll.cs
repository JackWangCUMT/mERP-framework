//using System;
//using System.Collections.Generic;
//using System.Text;
//using NHibernate;

//namespace Feng.NH
//{
//    /// <summary>
//    /// ����NHibernate�ļ�ҵ���߼��㣬��װ���ݷ��ʲ�
//    /// </summary>
//    public class NHibernateBll : IBaseBll
//    {
//        private IBaseDal m_dal = new NHibernateDal();

//        /// <summary>
//        /// ����
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Save(object entity)
//        {
//            m_dal.Save(entity);
//        }

//        /// <summary>
//        /// �޸�
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Update(object entity)
//        {
//            m_dal.Update(entity);
//        }

//        /// <summary>
//        /// ɾ��
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Delete(object entity)
//        {
//            m_dal.Delete(entity);
//        }

//        /// <summary>
//        /// ����
//        /// </summary>
//        public virtual void Clear()
//        {
//        }
//    }

//    /// <summary>
//    /// NHibernateDal
//    /// </summary>
//    public class NHibernateDal : IBaseDal
//    {
//        /// <summary>
//        /// ����
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Save(object entity)
//        {
//            using (var rep = RepositoryFactory.GenerateRepository(entity.GetType()))
//            {
//                try
//                {
//                    rep.BeginTransaction();
//                    rep.Save(entity);
//                    rep.CommitTransaction();
//                }
//                catch (Exception ex)
//                {
//                    rep.RollbackTransaction();
//                    if (ExceptionProcess.ProcessWithWrap(ex))
//                    {
//                        throw;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// �޸�
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Update(object entity)
//        {
//            using (var rep = RepositoryFactory.GenerateRepository(entity.GetType()))
//            {
//                try
//                {
//                    rep.BeginTransaction();
//                    rep.Update(entity);
//                    rep.CommitTransaction();
//                }
//                catch (Exception ex)
//                {
//                    rep.RollbackTransaction();
//                    if (ExceptionProcess.ProcessWithWrap(ex))
//                    {
//                        throw;
//                    }
//                }
//            }
//        }


//        /// <summary>
//        /// ɾ��
//        /// </summary>
//        /// <param name="entity"></param>
//        public virtual void Delete(object entity)
//        {
//            using (var rep = RepositoryFactory.GenerateRepository(entity.GetType()))
//            {
//                try
//                {
//                    rep.BeginTransaction();
//                    rep.Delete(entity);
//                    rep.CommitTransaction();
//                }
//                // delete will check not-null, but when in web grid, we only have id(and version) set back
//                catch (PropertyValueException)
//                {
//                    rep.RollbackTransaction();
//                    LoadandDelete(entity);
//                }
//                catch (Exception ex)
//                {
//                    rep.RollbackTransaction();
//                    if (ExceptionProcess.ProcessWithWrap(ex))
//                    {
//                        throw;
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// LoadandDelete
//        /// </summary>
//        /// <param name="entity"></param>
//        private void LoadandDelete(object entity)
//        {
//            using (var rep = RepositoryFactory.GenerateRepository(entity.GetType()))
//            {
//                try
//                {
//                    rep.Session.Lock(entity, LockMode.None);
//                    object id = rep.Session.GetIdentifier(entity);
//                    rep.Session.Evict(entity);
//                    entity = rep.Session.Get(entity.GetType(), id);
//                    rep.BeginTransaction();
//                    rep.Delete(entity);
//                    rep.CommitTransaction();
//                }
//                catch (Exception ex)
//                {
//                    rep.RollbackTransaction();
//                    if (ExceptionProcess.ProcessWithWrap(ex))
//                    {
//                        throw;
//                    }
//                }
//            }
//        }
//    }
//}