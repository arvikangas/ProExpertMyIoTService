using MediatR;
using MyIoTService.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace MyIoTService.Core.Commands.Handlers
{
    public class DeleteAccountHandler : AsyncRequestHandler<DeleteAccount>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        protected async override Task Handle(DeleteAccount request, CancellationToken cancellationToken)
        {
            await _accountRepository.Delete(request.Id);
        }
    }
}
