


//function selectHouseType(element) {
//    if (element.classList.contains("selected")) {
//        element.classList.remove("selected")
//        return
//    }

//    element.classList.add("selected")


//}



//const houseTypeGroup = document.getElementById("selectedIcons")

function selectHouseType(inputElement) {
    const element = inputElement.nextElementSibling;
    const isChecked = inputElement.checked;
    const houseType = element.getAttribute('data-housetype');
    if (isChecked) {
        element.classList.add("selected");
        sessionStorage.setItem(houseType, true);
    } else {
        element.classList.remove("selected");
        sessionStorage.setItem(houseType, false);
    }
}

function applyStoredCheckboxState() {
    const houseTypeInputs = document.querySelectorAll('.custom-control-input');
    houseTypeInputs.forEach((inputElement) => {
        const element = inputElement.nextElementSibling;
        const houseType = element.getAttribute('data-housetype');
        const storedState = sessionStorage.getItem(houseType);
        if (storedState === 'true') {
            inputElement.checked = true;
            element.classList.add("selected");
        } else {
            inputElement.checked = false;
            element.classList.remove("selected");
        }
    });
}

window.onload = function () {
    applyStoredCheckboxState();
    const houseTypeInputs = document.querySelectorAll('.custom-control-input');
    houseTypeInputs.forEach((inputElement) => {
        inputElement.addEventListener('change', () => selectHouseType(inputElement));
    });
}
