using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetAllAsync();
        return View(tasks);
    }

    public async Task<IActionResult> Edit(string id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, TaskItem task)
    {
        if (id != task.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(task);
        }

        var existingTask = await _taskService.GetByIdAsync(id);
        if (existingTask is null)
        {
            return NotFound();
        }

        task.CreatedAt = existingTask.CreatedAt;
        await _taskService.UpdateAsync(id, task);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        await _taskService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
