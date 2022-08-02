using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OWSData.Models.Composites;
using OWSData.Repositories.Interfaces;
using OWSShared.Interfaces;
using PirateRoyaltyPublicApi.Requests.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateRoyaltyPublicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly ICharactersRepository _charactersRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IHeaderCustomerGUID _customerGuid;

        public UsersController(ICharactersRepository charactersRepository,
           IUsersRepository usersRepository,
           IHeaderCustomerGUID customerGuid)
        {
            _usersRepository = usersRepository;
            _charactersRepository = charactersRepository;
            _customerGuid = customerGuid;
        }
        /// <summary>
        /// Update Character Mesh
        /// </summary>
        /// <remarks>
        /// Updates Character Mesh by Name and requires valid user session guid.
        /// </remarks>
        /// <param name="request">
        /// <b>CustomerGUID</b> - This is the API key.<br/>
        /// <b>UserSessionGUID</b> - This is a valid UserSessionGUID that contains CharacterName we need to update.<br/>
        /// <b>CharacterName</b> - This is the name of the character to update.<br/>
        /// <b>Mesh</b> - This is the mesh value we need to update.
        /// <b>Hair</b> - This is the hair value we need to update.
        /// </param>
        [HttpPost]
        [Route("UpdateCharacterMeshAndHair")]
        [Produces(typeof(SuccessAndErrorMessage))]
        public async Task<SuccessAndErrorMessage> UpdateCharacterMeshAndHair([FromBody] UpdateCharacterMeshRequest request)
        {
            request.SetData(_charactersRepository, _usersRepository, _customerGuid);
            return await request.Handle();
        }

        /// <summary>
        /// Get Cosmetic Character Data
        /// </summary>
        /// <remarks>
        /// Gets Cosmetic Character Data
        /// </remarks>
        /// <param name="request">
        /// <b>CustomerGUID</b> - This is the API key.<br/>
        /// <b>UserSessionGUID</b> - This is a valid UserSessionGUID that contains CharacterName we need to update.<br/>
        /// <b>CharacterName</b> - This is the name of the character we are trying to get the data out of.<br/>
        /// </param>
        [HttpPost]
        [Route("GetCosmeticCharacterData")]
        [Produces(typeof(CustomCharacterDataRows))]
        public async Task<CustomCharacterDataRows> GetCosmeticCharacterData([FromBody] GetCosmeticCharacterDataRequest request)
        {
            request.SetData(_charactersRepository, _usersRepository,_customerGuid);
            return await request.Handle();
        }
    }
}
