function generateOption(paramt){
    const option = document.createElement("option");
    option.value = paramt;
    option.innerHTML = paramt;
    const mainContainer=document.getElementById("categoryGame");
    mainContainer.appendChild(option)
}