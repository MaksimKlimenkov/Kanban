using System.Security.Claims;
using Kanban.Interfaces;
using Kanban.Models;
using Kanban.Repository.Extensions;
using Kanban.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Controllers;

public class TeamMemberController : ControllerBase
{
    private readonly IRepository<TeamMember> _teamMemberRepository;

    public TeamMemberController(IRepository<TeamMember> teamMemberRepository)
    {
        _teamMemberRepository = teamMemberRepository;
    }
}