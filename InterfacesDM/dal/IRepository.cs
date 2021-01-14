/*
*  ISEL-ADEETC-SI2
*   ND 2014-2020
*
*   Material didático para apoio 
*   à unidade curricular de 
*   Sistemas de Informação II
*
*	Os exemplos podem não ser completos e/ou totalmente correctos
*	sendo desenvolvido com objectivos pedagógicos
*	Eventuais incorrecções são alvo de discussão
*	nas aulas.
*/
using System.Collections.Generic;


namespace FSolv
{
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();//Possíveis Problemas de desempenho!!!
        IEnumerable<T> Find(System.Func<T, bool> criteria);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        bool Save();

    }
}
