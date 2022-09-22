using BirkanTuncer_PayCore_BitirmeProjesi.Base;
using BirkanTuncer_PayCore_BitirmeProjesi.Data;
using BirkanTuncer_PayCore_BitirmeProjesi.Dto;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Service
{
    public interface ITokenService
    {
        BaseResponse<TokenResponse> GenerateToken(TokenRequest tokenRequest);
    }
}
