import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService {
  async createKeep(keepData) {
    const response = await api.post('api/keeps', keepData)
    logger.log('created keep', response.data)
    const keep = new Keep(response.data)
    if (AppState.activeProfile === null || keep.creatorId === AppState.activeProfile.id) {
      AppState.keeps.unshift(keep)
    }
  }
  async deleteKeep(id) {
    const response = await api.delete(`api/keeps/${id}`)
    logger.log('deleted keep', response.data)
    const index = AppState.keeps.findIndex(keep => keep.id == id)
    AppState.keeps.splice(index, 1)
  }
  async getKeepById(keepId) {
    AppState.activeKeep = null
    const response = await api.get(`api/keeps/${keepId}`)
    // logger.log('here is your keep', response.data)
    const keep = new Keep(response.data)
    AppState.activeKeep = keep

  }
  async getAllKeeps() {
    const response = await api.get('api/keeps')
    // logger.log('here are your keeps', response.data)
    const keeps = response.data.map(pojo => new Keep(pojo))
    AppState.keeps = keeps

  }

}

export const keepsService = new KeepsService()