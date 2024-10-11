﻿using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IHttpServices
{
    public interface ITransferHttpService
    {
        //Task<List<TransferResponse>> GetAllTransfersByAccount(Guid accountId);

        //Metodo de prueba
        List<TransferResponse> GetAllTransfersByAccount(Guid accountId);
    }
}
