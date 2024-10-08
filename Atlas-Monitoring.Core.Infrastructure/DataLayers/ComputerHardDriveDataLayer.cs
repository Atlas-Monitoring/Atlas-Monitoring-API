﻿using Atlas_Monitoring.Core.Infrastructure.DataBases;
using Atlas_Monitoring.Core.Interface.Infrastructure;
using Atlas_Monitoring.Core.Models.Database;
using Atlas_Monitoring.Core.Models.ViewModels;
using Atlas_Monitoring.CustomException;
using Microsoft.EntityFrameworkCore;

namespace Atlas_Monitoring.Core.Infrastructure.DataLayers
{
    public class ComputerHardDriveDataLayer : IComputerHardDriveDataLayer
    {
        #region Properties
        private readonly DefaultDbContext _context;
        #endregion

        #region Constructor
        public ComputerHardDriveDataLayer(DefaultDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        #region Create
        public async Task<ComputerHardDrive> AddComputerHardDrive(ComputerHardDriveViewModel computerHardDriveView)
        {
            if (!await _context.Device.Where(item => item.Id == computerHardDriveView.ComputerId && item.DeviceType.Id == DeviceType.Computer.Id).AnyAsync())
            {
                throw new CustomDataBaseException($"Computer Id '{computerHardDriveView.ComputerId}' don't exist");
            }

            ComputerHardDrive computerHardDrive = new()
            {
                Id = Guid.NewGuid(),
                Device = await _context.Device.Where(item => item.Id == computerHardDriveView.ComputerId).SingleAsync(),
                Letter = computerHardDriveView.Letter,
                TotalSpace = computerHardDriveView.TotalSpace,
                SpaceUse = computerHardDriveView.SpaceUse,
                DateAdd = DateTime.Now,
                DateUpdate = DateTime.Now
            };

            await _context.ComputerHardDrive.AddAsync(computerHardDrive);
            await _context.SaveChangesAsync();

            return computerHardDrive;
        }
        #endregion

        #region Read
        public async Task<List<ComputerHardDriveViewModel>> GetAllComputerHardDriveOfAComputer(Guid computerId)
        {
            List<ComputerHardDrive> listComputerHardDrive = await _context.ComputerHardDrive.Where(item => item.Device.Id == computerId).ToListAsync();
            List<ComputerHardDriveViewModel> listComputerHardDriveViewModel = new();

            foreach (ComputerHardDrive computerHardDrive in listComputerHardDrive)
            {
                computerHardDrive.Device = new() { Id = computerId };
                listComputerHardDriveViewModel.Add(TransformComputerHardDriveToComputerHardDriveViewModel(computerHardDrive));
            }

            return listComputerHardDriveViewModel;
        }

        public async Task<Guid> GetGuidOfComputerHardDriveIfExist(Guid computerId, string letter)
        {
            if (await _context.ComputerHardDrive.Where(item => item.Device.Id == computerId && item.Letter == letter).AnyAsync())
            {
                ComputerHardDrive computerHardDrive = await _context.ComputerHardDrive.Where(item => item.Device.Id == computerId && item.Letter == letter).SingleAsync();
                return computerHardDrive.Id;
            }
            else
            {
                return Guid.Empty;
            }
        }
        #endregion

        #region Update
        public async Task<ComputerHardDriveViewModel> UpdateOneHardDrive(ComputerHardDriveViewModel computerHardDriveViewModel)
        {
            ComputerHardDrive computerHardDrive = await _context.ComputerHardDrive.Where(item => item.Id == computerHardDriveViewModel.Id).Include(item => item.Device).SingleAsync();
            computerHardDrive.Letter = computerHardDriveViewModel.Letter;
            computerHardDrive.TotalSpace = computerHardDriveViewModel.TotalSpace;
            computerHardDrive.SpaceUse = computerHardDriveViewModel.SpaceUse;
            computerHardDrive.DateUpdate = DateTime.Now;

            _context.Entry(computerHardDrive).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return TransformComputerHardDriveToComputerHardDriveViewModel(computerHardDrive);
        }
        #endregion

        #region Delete
        public async Task DeleteOneHardDriveOfAComputer(Guid computerId, Guid computerHardDriveId)
        {
            if (await _context.ComputerHardDrive.Where(item => item.Id == computerHardDriveId && item.Device.Id == computerId).AnyAsync())
            {
                ComputerHardDrive computerHardDrive = await _context.ComputerHardDrive.Where(item => item.Id == computerHardDriveId && item.Device.Id == computerId).SingleAsync();

                _context.ComputerHardDrive.Remove(computerHardDrive);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new CustomNoContentException($"Any HardDrive with id '{computerHardDriveId}' and computer id '{computerId}' was found");
            }

        }
        public async Task DeleteAllComputerHardDriveOfAComputer(Guid computerId)
        {
            List<ComputerHardDrive> listComputerHardDrive = await _context.ComputerHardDrive.Where(item => item.Device.Id == computerId).ToListAsync();

            foreach(ComputerHardDrive computerHardDrive in listComputerHardDrive)
            {
                _context.ComputerHardDrive.Remove(computerHardDrive);
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region Private Methods
        private ComputerHardDriveViewModel TransformComputerHardDriveToComputerHardDriveViewModel(ComputerHardDrive computerHardDrive)
        {
            return new()
            {
                Id = computerHardDrive.Id,
                ComputerId = computerHardDrive.Device.Id,
                Letter = computerHardDrive.Letter,
                TotalSpace= computerHardDrive.TotalSpace,
                SpaceUse = computerHardDrive.SpaceUse
            };
        }
        #endregion
    }
}
