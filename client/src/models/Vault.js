import { Profile } from "./Account.js"

export class Vault {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creator = new Profile(data.creator)
    this.name = data.name
  }
}

{
  const example = ` "name": "Cool Ideas",
  "description": "A Collection of all my favorite things",
  "img": "https://images.unsplash.com/photo-1595173425119-1c54835c1874?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=500&h=400&q=60",
  "isPrivate": false,
  "creatorId": "67d34fd816cbbe8474c4cd66",
  "creator": {
      "id": "67d34fd816cbbe8474c4cd66",
      "name": "Roman 😎",
      "picture": "https://images.unsplash.com/photo-1533597613087-680b561cc493?q=80&w=2948&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
      "coverImg": "https://images.unsplash.com/photo-1514923995763-768e52f5af87?q=80&w=3174&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
  },
  "id": 2,
  "createdAt": "2025-04-25T20:05:10",
  "updatedAt": "2025-04-25T20:05:10"`
}