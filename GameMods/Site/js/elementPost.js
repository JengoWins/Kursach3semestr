function generateElement(namePost,category,descript,date,imgPath){
    const contaiver = document.createElement("div");
    contaiver.style.margin = "20px";
    contaiver.style.border = "2px solid black";
    contaiver.style.padding = "5px";
    contaiver.style.margin = "20px";
    contaiver.style.width = "300px";
    contaiver.style.height = "420px";
    //Заголовок поста
    const header = document.createElement("h1");        
    const  headerText = document.createTextNode(namePost); 
    header.style.textAlign = "center";
    header.appendChild( headerText); 
    //Картинка
    const img = document.createElement("img");        
    img.src = imgPath;
    img.style.width="200px";
    img.style.height="200px";
    img.style.margin="auto 40px";
    //данные
    const p1 = document.createElement("p");        
    const  pText1 = document.createTextNode(category); 
    p1.appendChild( pText1);
    //данные
    const p2 = document.createElement("p");        
    const  pText2 = document.createTextNode(descript); 
    p2.appendChild( pText2);
    //данные
    const p3= document.createElement("p");        
    const  pText3 = document.createTextNode(date); 
    p3.appendChild( pText3);
    //данные
    const button = document.createElement("button");        
    const butText = document.createTextNode("Подробнее"); 
    button.style.margin = "auto 90px";
    button.onclick = function(){
        window.location='Post.html';
      };
    button.appendChild(butText);
    //Добавление компонентов
    contaiver.appendChild(header);
    contaiver.appendChild(img);
    contaiver.appendChild(p1);
    contaiver.appendChild(p2);
    contaiver.appendChild(p3);
    contaiver.appendChild(button);
    const mainContainer=document.getElementById("MainContainer");
    mainContainer.appendChild(contaiver)
}