using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Persons.DeletePerson;

public sealed class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result<string>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonCommandHandler(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByExpressionAsync(
            x => x.Id == request.Id && !x.IsDeleted,
            cancellationToken
        );

        if (person is null)
            return Result<string>.Failure("Personel bulunamadı veya zaten silinmiş.");

        person.IsDeleted = true;
        person.UpdatedAt = DateTime.UtcNow;
        person.UpdatedBy = "admin"; 

        _personRepository.Update(person);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Personel başarıyla silindi (soft delete).");
    }
}
