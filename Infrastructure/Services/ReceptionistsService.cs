using Application.DTOs.Incoming;
using Application.DTOs.Outgoing;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ReceptionistsService : IReceptionistsService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public ReceptionistsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<Guid> CreateReceptionistAsync(ReceptionistIncomingDto incomingDto)
        {
            var receptionistForCheck = await _repositoryManager.Receptionists.GetReceptionistByAccountIdAsync(incomingDto.AccountId, false);
            if (receptionistForCheck is not null)
                throw new EntityAlreadyExistsException();

            var receptionist = _mapper.Map<Receptionist>(incomingDto);
            await _repositoryManager.Receptionists.CreateReceptionistAsync(receptionist);
            await _repositoryManager.SaveChangesAsync();
            return receptionist.Id;
        }

        public async Task DeleteReceptionistByIdAsync(Guid receptionistId)
        {
            var receptionist = await _repositoryManager.Receptionists.GetReceptionistByIdAsync(receptionistId, false);
            if(receptionist is null)
                throw new EntityNotFoundException();
            _repositoryManager.Receptionists.DeleteReceptionist(receptionist);
            await _repositoryManager.SaveChangesAsync();
        }

        public async Task<ReceptionistOutgoingDto> GetReceptionistByIdAsync(Guid receptionistId)
        {
            var receptionist = await _repositoryManager.Receptionists.GetReceptionistByIdAsync(receptionistId, false);
            var outgoingReceptionist = _mapper.Map<ReceptionistOutgoingDto>(receptionist);
            return outgoingReceptionist;
        }

        public async Task<IEnumerable<ReceptionistOutgoingDto>> GetReceptionistsAsync()
        {
            var receptionists = await _repositoryManager.Receptionists.GetReceptionistsAsync();
            var outgoingReceptionists = _mapper.Map<IEnumerable<ReceptionistOutgoingDto>>(receptionists);
            return outgoingReceptionists;
        }

        public async Task UpdateReceptionistAsync(Guid receptionistId, ReceptionistIncomingDto incomingDto)
        {
            var receptionistForCheck = await _repositoryManager.Receptionists.GetReceptionistByIdAsync(receptionistId, false);
            if (receptionistForCheck is null)
                throw new EntityNotFoundException();

            var receptionist = _mapper.Map<Receptionist>(incomingDto);
            receptionist.Id = receptionistId;
            _repositoryManager.Receptionists.UpdateReceptionist(receptionist);
            await _repositoryManager.SaveChangesAsync();
        }
    }
}
