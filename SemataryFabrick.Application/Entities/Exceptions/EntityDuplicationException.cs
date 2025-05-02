namespace SemataryFabrick.Application.Entities.Exceptions;
public class EntityDuplicationException(string entityType, string argument, string paramName) 
    : Exception ($"Entity of type {entityType} with {paramName}: {argument} is already exist.")
{
}