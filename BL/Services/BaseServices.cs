using AutoMapper;
using BL.Contracts;
using DAL.Contracts;
using Domines;


namespace BL.Services
{
    public class BaseServices<T,DTO> : IBaseSerices<T, DTO> where T : BaseTable
    {
        readonly IGenericRepository<T> _redo;
        readonly IMapper _mapper;
        readonly IUserService _userServices;
        readonly IUnitOfWork _untofwork;
        public BaseServices(IGenericRepository<T> redo , IMapper mapper, IUserService UserService)
        {
            _mapper = mapper;
            _redo = redo;
            _userServices = UserService;
        }

        public BaseServices(IUnitOfWork untofwork, IMapper mapper, IUserService UserService)
        {
            _untofwork = untofwork;
            _redo = untofwork.Repository<T>();
            _mapper = mapper;
            _userServices = UserService;
        }


        public List<DTO> GetAll()
        {
            var LIST = _redo.GetAll();
            return _mapper.Map<List<T>, List<DTO>>(LIST);
        }

        public DTO GetById(Guid id)
        {
            var opj= _redo.GetById(id);
            return _mapper.Map<T,DTO>(opj); 
        }
        public bool Add(DTO entity) //, Guid userId
        {
            var dbopject= _mapper.Map<DTO,T>(entity);
            dbopject.CreatedBy = _userServices.GetLoggedInServices();
               // dbopject.CreatedBy = new Guid();
            dbopject.UpdatedDate = DateTime.Now;
            dbopject.CreatedDate = DateTime.Now;
       
            dbopject.CurrentState = 1;                                                                                                                      
            return _redo.Add(dbopject);
        }
        // Add Sender And Receiver in Shipming 
        public bool Add(DTO entity, out Guid id) //, Guid userId
        {
            var dbopject = _mapper.Map<DTO, T>(entity);
            dbopject.CreatedBy = _userServices.GetLoggedInServices();
            // dbopject.CreatedBy = new Guid();
            dbopject.UpdatedDate = DateTime.Now;
            dbopject.CreatedDate = DateTime.Now;

            dbopject.CurrentState = 1;
            return _redo.Add(dbopject,out id );
        }
        public bool Update(DTO entity, Guid userId)
        {
            var dbopject = _mapper.Map<DTO, T>(entity);
            dbopject.UpdatedBy=_userServices.GetLoggedInServices();
            dbopject.CurrentState = 1;
         //   dbopject.CreatedDate = DateTime.Now;
            return _redo.Update(dbopject);
        }
        public bool ChangeStatus(Guid id, Guid userId, int status = 1)
        {
            return _redo.ChangeStatus(id,_userServices.GetLoggedInServices() ,0);
        } 


    }
}
