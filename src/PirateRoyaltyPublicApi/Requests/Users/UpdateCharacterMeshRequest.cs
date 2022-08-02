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
    public class UpdateCharacterMeshRequest
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
            /// This is the name of the character to update.
            /// </remarks>
            public string CharacterName { get; set; }
            /// <summary>
            /// Mesh
            /// </summary>
            /// <remarks>
            /// This is the mesh value we need to update.
            /// </remarks>
            public string Mesh { get; set; }
            /// <summary>
            /// Hair
            /// </summary>
            /// <remarks>
            /// This is the hair value we need to update.
            /// </remarks>
            public string Hair { get; set; }

            private SuccessAndErrorMessage output;
            private Guid customerGUID;
            private ICharactersRepository charactersRepository;
            private IUsersRepository usersRepository;

            public void SetData(ICharactersRepository charactersRepository, IUsersRepository usersRepository, IHeaderCustomerGUID customerGuid)
            {
                this.usersRepository = usersRepository;
                this.charactersRepository = charactersRepository;
                customerGUID = customerGuid.CustomerGUID;
            }

            public async Task<SuccessAndErrorMessage> Handle()
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
                return new SuccessAndErrorMessage()
                {
                    Success = false,
                    ErrorMessage = "Could not find the Character Name"
                };
            }

            output = new SuccessAndErrorMessage();

            AddOrUpdateCustomCharacterData addOrUpdateCustomCharacterDataMesh = new AddOrUpdateCustomCharacterData()
            {
                CharacterName = CharacterName,
                CustomFieldName = "CharacterMeshType",
                FieldValue = Mesh
            };

            AddOrUpdateCustomCharacterData addOrUpdateCustomCharacterDataHair = new AddOrUpdateCustomCharacterData()
            {
                CharacterName = CharacterName,
                CustomFieldName = "CharacterHairType",
                FieldValue = Hair
            };

            await charactersRepository.AddOrUpdateCustomCharacterData(customerGUID, addOrUpdateCustomCharacterDataMesh);

            await charactersRepository.AddOrUpdateCustomCharacterData(customerGUID, addOrUpdateCustomCharacterDataHair);



            output.Success = true;
                output.ErrorMessage = "";

                return output;
            }
        }
}
