namespace Microsoft.Orleans.Course.SiloHost.Enums;

public enum PersistenceType
{
    None = 0,
    
    InMemory = 1,
    
    Redis = 2,
    
    MySql  = 3,
}