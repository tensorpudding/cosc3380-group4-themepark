const ChangeNavOnScroll = () => {
    const navbar = document.getElementById("main-nav");

    if (document.body.scrollTop > 80 || document.documentElement.scrollTop > 80) {
        navbar.style.setProperty("--nav-text-colors", "rgb(20, 11, 10)");
        navbar.style.backgroundColor = "var(--primary-background-color)";
        navbar.style.boxShadow = "var(--primary-nav-box-shadow)";
        navbar.style.color = "var(--secondary-text-color)";
    } else {

        navbar.style.setProperty("--nav-text-colors", "rgba(255, 255, 255, 1)");
        navbar.style.backgroundColor = "var(--top-nav-color)";
        navbar.style.boxShadow = "var(--top-nav-box-shadow)";
        navbar.style.color = "var(--primary-text-color)";
    }
}

// Drop Down menus
document.addEventListener("click", e => {
    const isDropDownButton = e.target.matches(".drop-down-button");
    if (!isDropDownButton && e.target.closest(".drop-down-container") != null) return;

    let currentDropDown;
    if (isDropDownButton) {
        currentDropDown = e.target.closest(".drop-down-container");
        console.log(currentDropDown);
        currentDropDown.classList.toggle("active");
    }

    document.querySelectorAll(".drop-down-container.active").forEach(dropdown => {
        if (dropdown === currentDropDown) return;

        dropdown.classList.remove("active");
    });
});
window.onscroll = () => {
    ChangeNavOnScroll();
}