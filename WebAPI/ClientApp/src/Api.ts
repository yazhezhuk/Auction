import {Note} from "./Models";

export abstract class Api{
    public static basePath = "/api/service/";

    public static uploadNewNote(note : Note) : void {
        console.log(note);
        fetch(this.basePath, {
            method: "POST",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(note)
        }).then(res => {
            console.log("Request complete! response:", res);
        }).catch(e => console.log(e));
    }

    public static deleteNote(note : Note) : void {
        console.log(JSON.stringify(note))
        fetch(this.basePath, {
            method: "DELETE",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(note)
        }).then(res => {
            console.log("Request complete! response:", res);
        });
    }

    public static updateNote(note : Note) : void {
        console.log(JSON.stringify(note))
        fetch(this.basePath, {
            method: "PUT",
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(note)
        }).then(res => {
            console.log("Request complete! response:", res);
        });
    }

    public static async fetchNotes() : Promise<Array<Note>>  {

        const response = await this.fetchPath()

        return (response.map((r: {id:number,text: string,date: string}) =>
            new Note(r.id,r.text,r.date))
        )
    }

    private static async fetchPath() : Promise<any> {
        const data = await fetch(Api.basePath)
        console.log(data)
        return await data.json()
    }
}