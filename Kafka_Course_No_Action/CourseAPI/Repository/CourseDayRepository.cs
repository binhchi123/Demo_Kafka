namespace CourseAPI.Repository
{
    public class CourseDayRepository : ICourseDayRepository
    {
        private readonly CourseDbContext _context;
        public CourseDayRepository(CourseDbContext context)
        {
            _context = context;
        }
        public async Task AddCourseDayAsync(CourseDay courseDay)
        {
            _context.CoursesDays.Add(courseDay);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountByCourseIdAsync(int courseId)
        {
            return await _context.CoursesDays.CountAsync(c => c.CourseId == courseId);
        }

        public async Task DeleteCourseDayAsync(CourseDay courseDay)
        {
            _context.CoursesDays.Remove(courseDay);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CourseDay>> GetAllCourseDayAsync()
        {
            return await _context.CoursesDays.ToListAsync();
        }

        public Task<CourseDay> GetCourseDayByIdAsync(int courseDayId)
        {
            return _context.CoursesDays.FirstOrDefaultAsync(c => c.CourseDayId == courseDayId);
        }

        public async Task UpdateCourseDayAsync(CourseDay courseDay)
        {
            _context.CoursesDays.Update(courseDay);
            await _context.SaveChangesAsync();
        }
    }
}
