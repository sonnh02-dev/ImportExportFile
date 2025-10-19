﻿using AutoMapper;
using ImportExportFile.Application.Abstractions.Files;
using ImportExportFile.Application.Dtos;
using ImportExportFile.Application.Dtos.Responses;
using ImportExportFile.Domain.Entities;
using ImportExportFile.Domain.IRepositories;
using ImportExportFile.Infrastructure.Files.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace ImportExportFile.Application.Services
{
    public class ImportService
    {
        private readonly IFileStorage _fileStorage;
        private readonly FileReaderFactory<BookDto> _bookReaderFactory;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;


        public ImportService(IFileStorage fileStorage, FileReaderFactory<BookDto> bookReaderFactory, IBookRepository bookRepository, IMapper mapper)
        {
            _fileStorage = fileStorage;
            _bookReaderFactory = bookReaderFactory;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<ImportResult> ImportBooksAsync(IFormFile file)
        {
            var filePath = await _fileStorage.SaveAsync(file);
            var extension = Path.GetExtension(filePath);
            var reader = _bookReaderFactory.GetReader(extension);
            var bookDtos = reader.Read(filePath);

            var books = _mapper.Map<List<Book>>(bookDtos);

            await _bookRepository.AddRangeAsync(books);
            await _bookRepository.SaveChangesAsync();
            // _fileStorage.Delete(filePath);

            return new ImportResult
            {
                TotalRecords = books.Count,
                SuccessCount = books.Count
            };
        }
    }
}
