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
*	
*	Baseado em: https://www.c-sharpcorner.com/UploadFile/rmcochran/C-Sharp-generic-identity-map-creating-an-object-pool-for-multiple-object-types/
*/
namespace FSolv.IndentityMap
{
    public interface IObjectPool
    {
        void AddItem<T>(T value, int key);
        void AddOrUpdate<T>(T value, int key);
        bool ContainsKey<T>(int key);
        T GetItem<T>(int key);
        bool RemoveItem<T>(int key);
    }
}