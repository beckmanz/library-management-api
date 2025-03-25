﻿using System.Runtime.InteropServices.JavaScript;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Reader;

public interface IReaderInterface
{
    Task<ResponseModel<ReaderModel>> AddReader(LibraryModel library, AddReaderRequestDto request);
    Task<ResponseModel<Object>> GetAllReaders(LibraryModel library);
    Task<ResponseModel<Object>> GetReaderById(LibraryModel library, Guid Id);
}