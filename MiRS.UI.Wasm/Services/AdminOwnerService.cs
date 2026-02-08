using MiRS.UI.Wasm.Domain.Dtos;
using MiRS.UI.Wasm.Domain.Entities;
using MiRS.UI.Wasm.Gateway.MiRSAdmin;
using MiRS.UI.Wasm.Models;

namespace MiRS.UI.Wasm.Services
{
    public class AdminOwnerService
    {
        private readonly IMiRSOwnerClient _client;
        private IList<Category> _gameMetadata = new List<Category>();

        public AdminOwnerService(IMiRSOwnerClient client)
        {
            _client = client;
        }

        public async Task<IList<Category>> GetGameMetadata(bool forceUpdate = false)
        {
            if (!_gameMetadata.Any() || forceUpdate)
            {
                await RefreshGameMetadata();
            }

            return _gameMetadata;
        }

        public async Task<IList<Category>> RefreshGameMetadata()
        {
            _gameMetadata = (await _client.GetGameMetadata()).ToList();
            return _gameMetadata;

        }

        public async Task AddNewTask(int catId, int levelNum)
        {
            IEnumerable<Category> metadata = await GetGameMetadata();

            Level level = metadata
                .FirstOrDefault(c => c.Id == catId)?
                .Levels.FirstOrDefault(l => l.Levelnumber == levelNum);

            if (level == null)
                throw new InvalidOperationException("Category or level not found");

            level.LevelTasks.Add(new LevelTask());
        }

        public async Task AddNewLevel(int catId)
        {
            IEnumerable<Category> metadata = await GetGameMetadata();

            Category category = metadata
                .FirstOrDefault(c => c.Id == catId);

            if (category == null)
                throw new InvalidOperationException("Category not found");

            category.Levels.Add(new Level());
        }

        public async Task AddNewCategory()
        {
            IList<Category> metadata = await GetGameMetadata();

            if (metadata == null)
                throw new InvalidOperationException("Data not found");

            metadata.Add(new Category());
        }

        // Temporary logic to find data by name: rework later (add some temporary id/Guid)
        public async Task DeleteTask(string catName, int levelNum, string taskName)
        {
            IEnumerable<Category> metadata = await GetGameMetadata();

            IEnumerable<Category> categories = metadata.Where(c => c.Name == catName);

            if (categories.Count() > 1)
                throw new InvalidOperationException("Category names must be distinct");

            Level level = categories.FirstOrDefault().Levels.FirstOrDefault(l => l.Levelnumber == levelNum);

            if (level == null)
                throw new InvalidOperationException("Category or level not found");

            level.LevelTasks.Remove(level.LevelTasks.Where(lt => lt.Name == taskName).FirstOrDefault());
        }

        public async Task DeleteLevel(string catName, int levelNum)
        {
            IEnumerable<Category> metadata = await GetGameMetadata();

            IEnumerable<Category> categories = metadata
                .Where(c => c.Name == catName);

            if (categories == null)
                throw new InvalidOperationException("Category not found");

            if (categories.Count() > 1)
                throw new InvalidOperationException("Category names must be distinct");

            Level level = categories.FirstOrDefault().Levels.FirstOrDefault(l => l.Levelnumber == levelNum);

            categories.FirstOrDefault().Levels.Remove(level);
        }

        public async Task DeleteCategory(string catName)
        {
            IList<Category> metadata = await GetGameMetadata();

            if (metadata == null)
                throw new InvalidOperationException("Data not found");

            IEnumerable<Category> items = metadata.Where(c => c.Name == catName);

            if (items.Count() > 1)
                throw new InvalidOperationException("Category names must be distinct");

            metadata.Remove(items.FirstOrDefault());
        }

        public async Task SaveChanges(AdminOwnerViewModel model)
        {

            CategoryContainer metadata = new CategoryContainer
            {
                Categories = model.Categories,
            };

            await _client.UpdateGameMetadata(metadata);
        }
    }
}
