using OWSData.Models.Composites;
using OWSData.Models.StoredProcs;
using OWSData.Repositories.Interfaces;
using OWSShared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PirateRoyaltyPublicApi.Requests.Users
{
    public class GetCosmeticCharacterDataRequest
    {
        /// <summary>
        /// User Session GUID
        /// </summary>
        /// <remarks>
        /// This is a valid UserSessionGUID that contains CharacterName we need to update.
        /// </remarks>
        public Guid UserSessionGUID { get; set; }
        /// <summary>
        /// Character Name
        /// </summary>
        /// <remarks>
        /// This is the name of the character to get Custom Data fields for.
        /// </remarks>
        public string CharacterName { get; set; }

        private Guid customerGUID;
        private ICharactersRepository charactersRepository;
        private IUsersRepository usersRepository;

        public void SetData(ICharactersRepository charactersRepository, IUsersRepository usersRepository, IHeaderCustomerGUID customerGuid)
        {
            this.usersRepository = usersRepository;
            this.charactersRepository = charactersRepository;
            customerGUID = customerGuid.CustomerGUID;
        }


        public async Task<CustomCharacterDataRows> Handle()
        {
            var AllCharactersInTheUserSession = await usersRepository.GetAllCharacters(customerGUID, UserSessionGUID);

            bool didWeFindTheCharacter = false;
            foreach (var currentCharacter in AllCharactersInTheUserSession)
            {
                if (currentCharacter.CharName == CharacterName)
                {
                    didWeFindTheCharacter = true;
                }
            }
            if (!didWeFindTheCharacter)
            {
                return null;
            }
        

            CustomCharacterDataRows output = new CustomCharacterDataRows();

                output.Rows = await charactersRepository.GetCustomCharacterData(customerGUID, CharacterName);

                return output;
            }

     }
}
