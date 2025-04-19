using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StreamingApp.Core.Commands.DB.CRUD.Interfaces;
using StreamingApp.DB;
using StreamingApp.Domain.Entities.Dtos;
using Stream = StreamingApp.Domain.Entities.InternalDB.Stream.Stream;

namespace StreamingApp.Core.Commands.DB.CRUD;
public class CRUDStreams : ICRUDStreams
{
    private readonly UnitOfWorkContext _unitOfWork;

    private readonly IMapper _mapper;

    public CRUDStreams(UnitOfWorkContext unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<StreamDto>> GetAll()
    {
        List<Stream> streams = _unitOfWork.StreamHistory.Include("GameCategories").Include("GameCategory").ToList();

        return streams.Select(_mapper.Map<StreamDto>).ToList();
    }

    public async Task<List<StreamDto>> CreateOrUpdtateAll(List<StreamDto> streams)
    {
        List<Stream> allStreams = _unitOfWork.StreamHistory.ToList();

        foreach (StreamDto stream in streams)
        {
            var foundStream = allStreams.FirstOrDefault(t => t.Id == stream.Id);

            if (foundStream != null)
            {
                var combinedData = _mapper.Map(foundStream, _mapper.Map<Stream>(stream));

                _unitOfWork.Update(combinedData);
            }
            else
            {
                var mappedData = _mapper.Map<Stream>(stream);

                await _unitOfWork.AddAsync(mappedData);
                allStreams.Add(mappedData);
            }
        }

        await _unitOfWork.SaveChangesAsync();

        return streams.Select(_mapper.Map<StreamDto>).ToList();
    }

    public async Task<bool> Delete(List<StreamDto> streams)
    {
        try
        {
            foreach (StreamDto stream in streams)
            {
                var removeData = await _unitOfWork.StreamHistory.FirstOrDefaultAsync(t => t.Id == stream.Id);

                if (removeData != null)
                {
                    _unitOfWork.Remove(removeData);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
