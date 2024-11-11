namespace StudentAPI.Service
{
    public class ClassService : IClassService
    {
        private readonly ILogger<ClassService> _logger;
        private readonly ClassMemory           _memory;
        private readonly IProducer             _producer;

        public ClassService(ILogger<ClassService> logger, ClassMemory memory, IProducer producer)
        {
            _logger   = logger;
            _memory   = memory;
            _producer = producer;
        }
        public Class DeleteClass(int id)
        {
            if (_memory.ClassesMemory.TryGetValue(id.ToString(), out var classes))
            {
                _memory.ClassesMemory.Remove(id.ToString());
                var request = new DeleteClassRequest { ClassId = classes.ClassId };
                _producer.Produce(request);
                return classes;
            }
            else
            {
                _logger.LogWarning($"Class with ID {id} not found");
                return null;
            }
        }

        public IEnumerable<Class> GetAllClass()
            => _memory.ClassesMemory.Values.ToList();

        public Class InsertClass(Class insertClass)
        {
            _memory.ClassesMemory.Add(insertClass.ClassId.ToString(), insertClass);
            var request = new InsertClassRequest
            {
                ClassId         = insertClass.ClassId,
                ClassName       = insertClass.ClassName,
                NumberOfStudent = insertClass.NumberOfStudent,
            };
            _producer.Produce(request);
            return insertClass;
        }

        public Class UpdateClass(Class updateClass)
        {
            if (_memory.ClassesMemory.TryGetValue(updateClass.ClassId.ToString(), out var existingClass))
            {
                _memory.ClassesMemory[updateClass.ClassId.ToString()] = updateClass;
                var request = new UpdateClassRequest
                {
                    ClassId         = updateClass.ClassId,
                    ClassName       = updateClass.ClassName,
                    NumberOfStudent = updateClass.NumberOfStudent,
                };
                _producer.Produce(request);
            }
            else
            {
                _logger.LogWarning($"Class with ID {updateClass.ClassId} not found");
                return null;
            }
            return updateClass;
        }
    }
}
