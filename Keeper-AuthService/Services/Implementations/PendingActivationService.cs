using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implementations
{
    public class PendingActivationService : IPendingActivationService
    {
        private readonly IPendingActivationsRepository _repository;
        private readonly IDTOMapper _mapper;

        public PendingActivationService(IPendingActivationsRepository repository,
            IDTOMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PendingActivationDTO?>> CreateAsync(CreatePendingActivationDTO createDTO)
        {
            PendingActivation pendingActivation = await _repository.CreateAsync(new PendingActivation
            {
                Email = createDTO.Email,
                PasswordHash = createDTO.PasswordHash,
                ActivationCodeHash = createDTO.ActivationCodeHash
            });

            PendingActivationDTO pendingActivationDTO = _mapper.Map(pendingActivation);
            return ServiceResponse<PendingActivationDTO?>.Success(pendingActivationDTO);
        }

        public async Task<ServiceResponse<PendingActivationDTO?>> GetByEmailAsync(string email)
        {
            PendingActivation? pendingActivation = await _repository.GetByEmailAsync(email);

            if (pendingActivation == null)
                return ServiceResponse<PendingActivationDTO?>.Fail(default, 404, "Registered user don't exist.");

            PendingActivationDTO pendingActivationDTO = _mapper.Map(pendingActivation);
            return ServiceResponse<PendingActivationDTO?>.Success(pendingActivationDTO);
        }

        public async Task<ServiceResponse<PendingActivationDTO?>> DeleteAsync(Guid id)
        {
            PendingActivation? pendingActivation = await _repository.GetByIdAsync(id);

            if (pendingActivation == null)
                return ServiceResponse<PendingActivationDTO?>.Fail(default, 404, "Pending activation doesn't exist.");
        
            pendingActivation = await _repository.DeleteAsync(id);

            PendingActivationDTO pendingActivationDTO= _mapper.Map(pendingActivation);

            return ServiceResponse<PendingActivationDTO?>.Success(pendingActivationDTO);
        }
    }
}
