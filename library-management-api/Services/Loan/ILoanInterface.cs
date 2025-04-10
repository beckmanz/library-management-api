﻿using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Loan;

public interface ILoanInterface
{
    Task<ResponseModel<LoanModel>> RegisterNewLoan(LibraryModel library, RegisterNewLoanRequestDto request);
    Task<ResponseModel<List<LoanResponseDto>>> GetAllLoans(LibraryModel library);
    Task<ResponseModel<LoanResponseDto>> GetLoan(LibraryModel library, Guid id);
    Task<ResponseModel<LoanResponseDto>> ReturnBook(LibraryModel library, Guid id);
}