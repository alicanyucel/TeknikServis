using AutoMapper;
using GenericRepository;
using MediatR;
using TeknikServis.Domain.Entities;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.UpdatePerson;

internal sealed class UpdatePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdatePersonCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        Person? person = await personRepository.GetByExpressionWithTrackingAsync(P => P.Id == request.Id, cancellationToken);
        if( person == null)
        {
            return Result<string>.Failure("personel bulunamadi.");
        }
        mapper.Map(request,person);
        personRepository.Update(person);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return "Personel güncellendi.";

    }
}