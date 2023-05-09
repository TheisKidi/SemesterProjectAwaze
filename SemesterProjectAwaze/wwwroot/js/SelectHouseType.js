


function selectHouseType(element) {
    if (element.classList.contains("selected")) {
        element.classList.remove("selected")
        return
    }

    element.classList.add("selected")


}



const houseTypeGroup = document.getElementById("selectedIcons")