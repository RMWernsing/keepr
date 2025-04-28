import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { AppState } from "@/AppState.js"
import { Vault } from "@/models/Vault.js"

class VaultsService {
  async createVault(vaultData) {
    const response = await api.post('api/vaults', vaultData)
    logger.log('here is your vault', response.data)
    const vault = new Vault(response.data)
    if (AppState.activeProfile === null || vault.creatorId === AppState.activeProfile.id) {
      AppState.vaults.push(vault)
    }
  }

}

export const vaultsService = new VaultsService()