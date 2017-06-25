@if (SignInManager.IsSignedIn(User)) {
    <form asp-area="" asp-controller="Account" asp-action="LogOff" method="post" id="logoutForm">
        <ul class="nav navbar-nav">
            <li class="@if (ViewData[" Title"].ToString() == "Manage your account" ||ViewData["Title"].ToString() ==  "Register" || ViewData["Title"].ToString() == "Change Password"){<text>active</text>}">
                <a asp-area="" class="dropdown-toggle" data-toggle="dropdown" title="Manage">Hello @UserManager.GetUserName(User)!</a>
            <ul class="dropdown-menu">
                <li class="@if (ViewData[" Title"].ToString() == "Manage your account"){<text>active</text>}"><a asp-area="" asp-controller="Manage" asp-action="Index">My Account</a></li>
            <li class="@if (ViewData[" Title"].ToString() == "Change Password"){<text>active</text>}"><a asp-area="" asp-controller="Manage" asp-action="ChangePassword">Change Password</a></li>
        <li><!a href="javascript:document.forms.item(0).submit();">Sign Out</!a></li>
                </ul >
            </li >          
        </ul >
        <ul class="nav navbar-nav">
            <li class="@if (ViewData[" Title"].ToString() == "Add Stuff"){<text>active</text>}"><a asp-area="" asp-controller="Stuff" asp-action="Index">Add Stuff</a></li>
        </ul >
    </form >
}
else {
    <ul class="nav navbar-nav">
        <li class="@if (ViewData[" Title"].ToString() == "Register"){<text>active</text>}"><a asp-area="" asp-controller="Account" asp-action="Register">Register</a></li>
        <li class="@if (ViewData[" Title"].ToString() == "Log in " || ViewData["Title"].ToString() == "Forgot your password? "){<text>active</text>}" > <a asp-area="" asp-controller="Account" asp-action="Login">Log in</a></li >
    </ul >
}