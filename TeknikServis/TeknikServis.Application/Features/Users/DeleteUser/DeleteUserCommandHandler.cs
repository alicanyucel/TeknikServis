using GenericRepository;
using MediatR;
using TeknikServis.Domain.Repositories;
using TS.Result;

namespace TeknikServis.Application.Features.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExpressionAsync(
            x => x.Id == request.Id,
            cancellationToken
        );

        if (user == null)
            return Result<string>.Failure("Kullanıcı bulunamadı.");
        _userRepository.Delete(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return "Kullanıcı silindi";
    }
}