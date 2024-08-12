using Microsoft.VisualBasic;
using System.Net;

namespace Shop_BFF.DTOs
{
    public class mapperdemo
    {

    }
    //public async Task<MessageDto> CreateCandidate(string applicationGuid, string tenantGuid, string userGuid, CreateCandidateDto candidate)
    //{
    //    // gather ids and guids for feature operation
    //    var featureDto = await CheckIdsAndGuids(candidate, tenantGuid, userGuid);

    //    #region Global Service Integration
    //    var globalDto = new CreateCandidateByClientGlobalDto()
    //    {
    //        FirstName = candidate.FirstName,
    //        MiddleName = candidate.MiddleName,
    //        LastName = candidate.LastName,
    //        Username = candidate.Username,
    //        DateOfBirth = candidate.DateOfBirth,
    //        Ssn = candidate.Ssn,
    //        GenderId = (short?)Convert.ToInt32(candidate.GenderId),
    //        Emails = new List<CreateEmailGlobalDto>()
    //        {
    //            new CreateEmailGlobalDto(){  EmailAddressTitle = candidate.Email,IsPrimary = true }
    //        },
    //        PhoneNumbers = new List<CreatePhoneNumberGlobalDto>()
    //        {
    //            new CreatePhoneNumberGlobalDto()
    //            {
    //                PhoneNumberValue = candidate.MobileNumber,
    //                IsPrimary = true
    //            }
    //        },
    //        Addresses = new List<CreateAddressGlobalDto>()
    //        {
    //            new CreateAddressGlobalDto()
    //            {
    //                AddressTitle = candidate.Address,
    //                IsPrimary = true
    //            }
    //        },
    //        PersonGroups = new List<PersonGroupGlobalDto>()
    //        {
    //            new PersonGroupGlobalDto()
    //            {
    //                GroupName = "111EB3C7-9BDB-4D77-8B28-5222AC5BF9DE"
    //            }
    //        },
    //        PersonTypeId = (int)PersonTypeGlobal.Candidate,    // candidate
    //        DemographicId = 1    // Temprary
    //    };

    //    var GlobalUser = await AddCandidateGlobalService(globalDto, applicationGuid, tenantGuid, userGuid);

    //    if (GlobalUser == null) { throw new ExamRoomException($"User Not Added : {GlobalUser}", HttpStatusCode.BadRequest); }

    //    string candidateShellApplicationGuid = "C4292201-677F-40E2-B26B-2B7BEA6F9692";

    //    AddCandidatePermissionDto dto = new AddCandidatePermissionDto();
    //    dto.UserGuid = GlobalUser.PersonGuid;

    //    await AddCandidatePermission(dto, candidateShellApplicationGuid, tenantGuid, userGuid);

    //    #endregion

    //    var candidateDto = _mapper.Map<DisplayCandidateDto>(candidate);

    //    IQueryable<MenuErRole> permissionsQuery = _context.MenuErRoles.AsNoTracking()
    //        .Where(p => p.ErRoleId == (int)Enumerations.LocalRoles.Candidate && p.TenantMetaInformationId == featureDto.TenantMetaInformationId && p.Active == true);

    //    var permissions = await permissionsQuery.ToListAsync();

    //    // form new PersonMetaInformation for the new candiate
    //    var newPersonMetaInformation = GetNewPersonMetaInformation(candidate, featureDto.TenantMetaInformationId, featureDto.UserId, GlobalUser.PersonGuid);

    //    await _context.PersonMetaInformations.AddAsync(newPersonMetaInformation);

    //    // execute first phase of candiate creation
    //    var createCandidateOperation = await _context.SaveChangesAsync() > 0;

    //    if (createCandidateOperation == false) throw new ExamRoomException(Constants.CandidateNotCreated, HttpStatusCode.BadRequest);

    //    // add extra details; Email, Address, Phone Number,
    //    await AddExtraDetails(newPersonMetaInformation, featureDto.TenantMetaInformationId, featureDto.UserId, candidate);

    //    // link the candidate to a role
    //    var newPersonMetaRole = await GetNewRolePerson(candidate, newPersonMetaInformation, featureDto);

    //    // populate the display dto for API response
    //    candidateDto.Groups = await AddNewPersonGroups(candidate, newPersonMetaInformation, featureDto);

    //    // link the candidate to a tenant
    //    // based on the tenant hierachy, starting from the bottom
    //    // a candidate is linked to the first available tenant for amongst; subjects, courses, departments, campuses and universities
    //    await CreateLinkToTenant(candidate, newPersonMetaInformation, featureDto, candidateDto, newPersonMetaRole);

    //    // link the new candidate to a language
    //    await AddNewPersonLanguage(newPersonMetaInformation, featureDto);

    //    var linkCandidateToTenantOperation = await _context.SaveChangesAsync();

    //    // final mapping for the display object
    //    candidateDto.CountryGuid = candidate.CountryGuid;
    //    candidateDto.PersonMetaInformationId = newPersonMetaInformation.PersonMetaInformationId;
    //    candidateDto.PersonGuid = newPersonMetaInformation.PersonGuid.ToString();
    //    candidateDto.Language = featureDto.LanguageName;
    //    candidateDto.TenantInformationGuid = tenantGuid;
    //    candidateDto.Number.Add(candidate.MobileNumber);
    //    candidateDto.Email.Add(candidate.Email);
    //    candidateDto.Address.Add(candidate.Address);

    //    // add more role details to the display dto
    //    candidateDto.Role = await AddRoleDetailToDisplay(featureDto.Role, permissions);

    //    var message = new MessageDto(linkCandidateToTenantOperation, Constants.InsertMessage, Constants.NotInsertMessage);
    //    message.Record = candidateDto;

    //    return message;
    //}
}
