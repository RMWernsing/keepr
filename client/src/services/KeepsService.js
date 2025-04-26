import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService {
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